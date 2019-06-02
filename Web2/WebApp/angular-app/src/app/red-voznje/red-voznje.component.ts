import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-red-voznje',
  templateUrl: './red-voznje.component.html',
  styleUrls: ['./red-voznje.component.css']
})
export class RedVoznjeComponent implements OnInit {

  redVoznjeForm = this.fb.group({
    redvoznje: ['', Validators.required],
    dan: ['', Validators.required],
    linija: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService:MainServiceService) { }

  get f() { return this.redVoznjeForm.controls; }

  ngOnInit() {
  }

  onSubmit() {
    console.warn(this.redVoznjeForm.value);
  }

}
