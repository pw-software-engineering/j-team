import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ClientClient, ClientLoginCmd} from "../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private client: ClientClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  get f() { return this.form.controls; }

  onSubmit(): void {
    this.submitted = true;
    const loginRequest = this.client.login(new ClientLoginCmd(this.form.value));
    loginRequest.subscribe(
      {
        next: (value) => {
          console.log(value);
          localStorage.setItem('x-client-token', value);
          this.router.navigate(['/hotels'], {relativeTo: this.route});
        },
        error: (error) => {
          alert(error.response);
          console.log(error.response);
        }
      }
    );
  }
}
