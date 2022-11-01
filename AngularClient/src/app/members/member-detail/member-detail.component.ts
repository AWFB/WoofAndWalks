import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member: Member;
  activeTab: TabDirective;
  messages: Message[] = [];

  constructor(
    private memberService: MembersService,
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}

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
    if (this.activeTab.heading === 'Messages' &&this.messages.length === 0) {
      this.loadMessages()
    }
  }
}
