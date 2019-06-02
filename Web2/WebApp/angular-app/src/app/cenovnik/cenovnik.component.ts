import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {

  cenovnikForm = this.fb.group({
    tipkarte: ['', Validators.required],
    tipputnika: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService:MainServiceService) { }

  get f() { return this.cenovnikForm.controls; }

  ngOnInit() {
  }
   

  onSubmit() {
    console.warn(this.cenovnikForm.value);
  }

}
