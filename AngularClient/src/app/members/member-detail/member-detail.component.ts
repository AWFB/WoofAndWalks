import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';
import {PresenceService} from "../../_services/presence.service";
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";
import {take} from "rxjs";

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit, OnDestroy {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member: Member;
  activeTab: TabDirective;
  messages: Message[] = [];
  user: User;

  constructor(
    // private memberService: MembersService,
    public presence: PresenceService,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private accountService: AccountService,
    private router: Router) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);

      // this is needed to prevent angular reusing routes which prevents messages
      // from being loaded when toast is clicked to take user to messages
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }



  ngOnInit(): void {
    this.route.data.subscribe(data => {
        this.member = data['member']
    })

    this.route.queryParams.subscribe(params => {
        params['tab'] ? this.selectTab(params['tab']) : this.selectTab(0);
    })
  }

//   loadMember() {
//     this.memberService
//       .getMember(this.route.snapshot.paramMap.get('username'))
//       .subscribe((member) => {
//         this.member = member;
//       });
//   }

  loadMessages() {
    this.messageService
      .getMessageThread(this.member.username)
      .subscribe((messages) => {
        this.messages = messages;
      });
  }

  selectTab(tabId: number) {
    // handles router to correct tab on profile hover in matches
    this.memberTabs.tabs[tabId].active = true
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) {
      this.messageService.createHubConnection(this.user, this.member.username);
    } else {
      this.messageService.stopHubConnection();
    }
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
