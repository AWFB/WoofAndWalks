import { Component, Directive, ElementRef, HostListener, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from '../_services/members.service';
import { ActivatedRoute } from '@angular/router';

@Directive({
    selector: '[imgSelect]'
})
export class GalleryDirective {

    constructor(private ele: ElementRef) {}
    // I suspect there is a cleaner way of doing this with ngClass 
    @HostListener('click')
    imageChange(){
        var src:any = this.ele.nativeElement.src;
        var preview:any = document.getElementById("preview");
        preview.src = src;
        
        var imageSlide = document.getElementsByClassName('img-slide');
        for(let i = 0; i<imageSlide.length; i++) {
            imageSlide[i].classList.remove("active");
        }
        
        this.ele.nativeElement.parentElement.classList.add('active')
    }
}

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css'],
})
export class GalleryComponent implements OnInit {
  member: Member;

  constructor(
    private memberService: MembersService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    this.memberService
      .getMember(this.route.snapshot.paramMap.get('username'))
      .subscribe((member) => {
        this.member = member;
      });
  }
}
