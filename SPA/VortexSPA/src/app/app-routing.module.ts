import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './components//log-in/log-in.component'
import { OverviewComponent } from './components/overview/overview.component';
import { AuthGuard } from './auth.guard';


const routes: Routes = [
  { path:'', component: OverviewComponent, canActivate: [AuthGuard] },
  { path:'login', component: LogInComponent },
  
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
