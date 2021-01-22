import { Injectable } from '@angular/core';
import { SocialUser } from 'angularx-social-login';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private user: SocialUser;
  public userObservable = new Subject<SocialUser>();

  constructor() { }

  public setUser(puser:SocialUser) {
    this.user = puser;
    window.sessionStorage.setItem('user', JSON.stringify(this.user));
    this.userObservable.next(puser);
  }

  public getUser() {
    let u = window.sessionStorage.getItem('user');

    if (u != undefined) {
      this.user = JSON.parse(u);
      return this.user;
    }

    return null;
  }




}
