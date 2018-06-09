import { Component, Inject, Injectable } from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA, MatDialog} from '@angular/material';

@Component({
  selector: 'pr-error',
  templateUrl: './errorDialog.component.html',
  styleUrls: ['./errorDialog.component.css']
})
export class ErrorDialogComponent {

  errorMsg = "";

  constructor(private dialogRef: MatDialogRef<ErrorDialogComponent>, @Inject(MAT_DIALOG_DATA) public data : any) { 
    this.errorMsg = data.errorMsg;
  }

  public closeDialog(){
    this.dialogRef.close();
  }

}
