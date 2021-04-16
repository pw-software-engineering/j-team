import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-offers-add',
  templateUrl: './offers-add.component.html',
  styleUrls: ['./offers-add.component.scss']
})
export class OffersAddComponent implements OnInit {

    form!: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder
    ) {}

    ngOnInit() {

        this.form = this.formBuilder.group({
            title: ['', Validators.required],
            costPerChild: ['', Validators.required],
            costPerAdult: ['', Validators.required],
            maxGuests: ['', [Validators.required]],
        });
    }

    // // convenience getter for easy access to form fields
    // get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        // // reset alerts on submit
        // this.alertService.clear();

        // // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        // if (this.isAddMode) {
        //     this.createUser();
        // } else {
        //     this.updateUser();
        // }
    }

    // private createUser() {
    //     this.userService.create(this.form.value)
    //         .pipe(first())
    //         .subscribe(() => {
    //             this.alertService.success('User added', { keepAfterRouteChange: true });
    //             this.router.navigate(['../'], { relativeTo: this.route });
    //         })
    //         .add(() => this.loading = false);
    // }

}
