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
          console.log(JSON.stringify(value));
          localStorage.setItem('x-client-token',JSON.stringify(value));
          var a = localStorage.getItem('x-client-token');
          console.log(a);
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
export function GetClientToken(){
    let token = localStorage.getItem('x-client-token');
    if (token == null)
      token = "";
    return token;
}
