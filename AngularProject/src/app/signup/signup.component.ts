import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { RegisterService } from '../Services/register.service';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { NgOtpInputModule } from 'ng-otp-input';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  users: any
  Roles:any

  RegisterForm = this.fb.group({
    first_name: [''],
    last_name: [''],
    email: [''],
    mobile_no: [''],
    password: [''],
    confirm_password: [''],
    role: ['']
  })
  CustomValidators: any;


  
  constructor(
    private fb: FormBuilder,
    private registerService: RegisterService,
    private route: Router
  ) { }

  ngOnInit() {
this.getRoles();

    this.RegisterForm = this.fb.group({
      first_name:['', [Validators.required, Validators.pattern('[a-zA-Z]*')]],
      last_name:['', [Validators.required, Validators.pattern('[a-zA-Z]*')]],
      email:['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]],
      mobile_no:['', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]],
      password:['', [Validators.required, Validators.pattern('[0-9a-zA-Z@!#$%^&*()_+]*'), Validators.minLength(6)]],
      confirm_password:['', [Validators.required]],
      role:['',Validators.required]
    },
    {validator: this.checkIfMatchingPasswords('password', 'confirm_password')},
  )}

 
  getUsers() {
    this.registerService.getUsers().subscribe(
      (response: any) => {
        this.users = response
        console.log(this.users);
      }
    )
  }
  getRoles() {  
    this.registerService.getRoles().subscribe(
      (response: any) => {
        this.Roles = response
        console.log(this.Roles);
} 
    )
}
  checkIfMatchingPasswords(passwordKey: string, passwordConfirmationKey: string) {
    return (group: FormGroup) => {
      let passwordInput = group.controls[passwordKey],
          passwordConfirmationInput = group.controls[passwordConfirmationKey];
      if (passwordInput.value !== passwordConfirmationInput.value) {
        return passwordConfirmationInput.setErrors({notEquivalent: true})
      }
      else {
          return passwordConfirmationInput.setErrors(null);
      }
    }
  }
  AddUser() {
debugger;
    console.log(this.RegisterForm);
    // if(this.RegisterForm.valid)
    // {
    //   alert(`Thank you ${this.RegisterForm}`);
    //   this.RegisterForm.reset(); //reset form value
    // }
    if (this.RegisterForm.value.password != this.RegisterForm.value.confirm_password) {
      return (
        this.RegisterForm.getError('mismatch') &&
        this.RegisterForm.get('confirm_password')?.touched
      );
    }
    const signup = {
      firstName: this.RegisterForm.value.first_name,
      lastName: this.RegisterForm.value.last_name,
      email: this.RegisterForm.value.email,
      mobile: this.RegisterForm.value.mobile_no,
      password: this.RegisterForm.value.password,
      confirmPassword: this.RegisterForm.value.confirm_password,
      role:this.RegisterForm.value.role
    }
    this.registerService.addUsers(signup).subscribe(
      data => {
        if (data) {

          this.route.navigate(['/Home'])
        }
        else {
          Swal.fire("user already exists, please login to continue")
        }
        console.log(data)
      },
      err => {
        console.log(err)
      }

    )
  }
}

