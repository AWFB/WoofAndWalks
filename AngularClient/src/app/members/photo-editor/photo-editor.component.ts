import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/Photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css'],
})
export class PhotoEditorComponent implements OnInit {
  @Input() member: Member;
  selectedFile: File = null;
  baseUrl = environment.apiUrl;
  user: User;

  constructor(
    private http: HttpClient,
    private accountService: AccountService,
    private memberService: MembersService
  ) {
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
  }

  ngOnInit(): void {}

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  onUpload() {
    const httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.user.token,
      }),
    };

    const fd = new FormData();
    fd.append('file', this.selectedFile, this.selectedFile.name);

    this.http
      .post(this.baseUrl + 'users/add-photo', fd, httpOptions)
      .subscribe((photo: Photo) => {
        this.member.photos.push(photo);

        // refresh profile picture in nav and edit profile on first upload
        if (photo.isMain) {
            this.user.photoUrl = photo.url
            this.member.photoUrl = photo.url
            this.accountService.setCurrentUser(this.user);
        }
      });
  }

  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user); //update current user and photo inside local storage for nav bar
      this.member.photoUrl = photo.url;
      
      // loop through each photo and set main photo to false
      // set the passed in photo.id to main profile photo
      this.member.photos.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      })
    });
  }

  deletePhoto(photoId: number) {
    this.memberService.deletePhoto(photoId).subscribe(() => {
        this.member.photos = this.member.photos.filter(x => x.id !== photoId);
    })
  }
}
