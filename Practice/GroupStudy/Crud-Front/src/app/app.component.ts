import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Crud-Front';

  onClick(){
    console.log("button clicked");
  }
  printMessager() {
    console.log("clicked");
    }
  
}
