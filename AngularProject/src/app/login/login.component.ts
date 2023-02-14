import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../Services/register.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin!: boolean;
  showPassword!: boolean ;
  show = false;

  constructor(
    private registerService: RegisterService,
    private route: Router,
  ) { }

  login = {
    email: '',
    password: ''
  }
  forgotemail = {
    email: ''
  }
  ngOnInit(): void {
   
  }
 
  loginAccess() {
    const login = {
      email: this.login.email,
      password: this.login.password
    }
    //var data = console.log(this.loginForm.email);
    this.registerService.loginAccess(login).subscribe(
      (data:any) => {
        if (data) {
            this.route.navigate(['/Home']) 
        }
        else {
          Swal.fire("Invalid email");
        }
        console.log(data);
      },
      err => {
        console.log(err)
      }
    )
  }

  forgotpassword(){
        const email = {
         email: this.forgotemail.email
        }
      this.registerService.getemail(email).subscribe(
        (data:any) => {
          if (data) {
              const token = data.jwtToken;
              localStorage.setItem("Token", token);
              this.invalidLogin = false;
              this.route.navigate(['/Home']) 
          }
          else {
            Swal.fire("new user signup to continue");
          }
          console.log(data);
        },
        err => {
          console.log(err)
        }
      )
  }
}
