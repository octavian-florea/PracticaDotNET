import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from "@angular/material";
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent {
  title = 'app';

  constructor(private dialog: MatDialog) {}

  openRegistrationDialog() {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.disableClose = false;
      this.dialog.open(RegisterComponent, dialogConfig);
  }

  openLogInDialog(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = false;
    this.dialog.open(LoginComponent, dialogConfig);
  }


}
