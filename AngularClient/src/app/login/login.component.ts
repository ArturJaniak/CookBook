
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../shared/services/authentication.service';
import { UserForAuthenticationDto } from '../_interfaces/user/userForAuthenticationDto.model';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  public errorMessage: string = '';
  public showError: boolean;
  public isUserAuthenticated: boolean;
  private _returnUrl: string;
  constructor(private _authService: AuthenticationService, private _router: Router, private _route: ActivatedRoute) { }
  ngOnInit(): void {
    this._authService.authChanged
    .subscribe(res => {
      this.isUserAuthenticated = res;
    })
    this.loginForm = new FormGroup({
      username: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
      
    })
    this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }
  public validateControl = (controlName: string) => {
    return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName)
  }
  public loginUser = (loginFormValue) => {
    this.showError = false;
    const login = {... loginFormValue };
    const userForAuth: UserForAuthenticationDto = {
      email: login.username,
      password: login.password
    }
    this._authService.loginUser('api/accounts/login', userForAuth)
    .subscribe((res:any) => {
       localStorage.setItem("token", res.token);
       this._authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
       this._router.navigate([this._returnUrl]);
       
    },
    (error) => {
      this.errorMessage = error;
      this.showError = true;
    })
  }
  public logout = () => {
    this._authService.logout();
    this._router.navigate(["/"]);
  }
  

  
}