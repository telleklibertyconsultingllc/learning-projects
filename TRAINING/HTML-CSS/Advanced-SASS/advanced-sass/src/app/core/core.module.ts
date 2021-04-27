import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FooterComponent } from './footer/footer.component';
import { MainComponent } from './main/main.component';
import { MenuComponent } from './menu/menu.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  declarations: [
    FooterComponent,
    MainComponent,
    MenuComponent
  ],
  exports: [
    FooterComponent,
    MainComponent,
    MenuComponent
  ]
})
export class CoreModule {
}
