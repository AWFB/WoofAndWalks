import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import {ConfirmService} from "../_services/confirm.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {

  constructor(private confirmService: ConfirmService) {
  }
// route guard will auto subscribe
  canDeactivate(
    component: MemberEditComponent):  Observable<boolean> | boolean { //union type
        if (component.editForm.dirty) {
            return this.confirmService.confirm()
        }
    return true;
  }

}
