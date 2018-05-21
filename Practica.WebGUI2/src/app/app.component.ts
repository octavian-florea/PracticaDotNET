import { Component } from '@angular/core';
import { ActivityService } from './services/activity.service';
import { AuthService } from "./services/auth.service";
import { ProfileService } from './services/profile.service';
import { CatalogService } from './services/catalog.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ActivityService, AuthService, ProfileService, CatalogService]
})
export class AppComponent {
  title = 'app';
}
