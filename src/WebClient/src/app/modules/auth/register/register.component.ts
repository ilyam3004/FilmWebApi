import {Component, OnInit} from '@angular/core';
import {first} from "rxjs";
import {FormBuilder, FormControl, FormGroup, ValidatorFn, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {AlertService} from "../../../shared/services/alert.service";
import {AccountService} from "../../../core/services/account.service";
import {RegisterRequest} from "../../../core/models/user";

@Component({
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]],
    }, { validator: this.passwordMatchValidator() });
  }

  passwordMatchValidator() {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls['password'];
      const matchingControl = formGroup.controls['confirmPassword'];

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ passwordsNotMatch: true });
        return;
      } else {
        matchingControl.setErrors(null);
      }
    }
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.form.invalid) {
      return;
    }

    let request: RegisterRequest = {
      login: this.form.value['email'],
      password: this.form.value['password'],
      confirmPassword: this.form.value['confirmPassword']
    }

    console.log(request);

    this.loading = true;
    this.accountService.register(request)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', 
              { keepAfterRouteChange: true, autoClose: true });
          this.router.navigate(['../login'], {relativeTo: this.route});
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }
}
