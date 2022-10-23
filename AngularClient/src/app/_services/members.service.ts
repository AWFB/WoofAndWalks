import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
    baseUrl = environment.apiUrl;
    members: Member[] = []
    paginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http: HttpClient) { }

  getAllMembers(page?: number, itemsPerpage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerpage !== null) {
        params = params.append('pageNumber', page.toString());
        params = params.append('pageSize', itemsPerpage.toString());
    }
    
    return this.http.get<Member[]>(this.baseUrl + 'users', {observe: 'response', params})
    .pipe(map(response => {
        this.paginatedResult.result = response.body;
        if (response.headers.get('pagination') != null) {
            this.paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
        }
        return this.paginatedResult;
    }))
  
  }

  getMember(username: string) {
    const member = this.members.find(x => x.username === username)
    if (member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
        map(() => {
            const index = this.members.indexOf(member);
            this.members[index] = member;
        })
    )
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {}) // empty object due to put request
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId)
  }

}
