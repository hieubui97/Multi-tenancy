import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  book = { items: [], totalCount: 0 } as any;

  constructor() {}

  ngOnInit() {
  }
}
