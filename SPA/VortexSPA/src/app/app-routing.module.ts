import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './components//log-in/log-in.component'
import { OverviewComponent } from './components/overview/overview.component';
import { AuthGuard } from './auth.guard';
import { StatisticsComponent } from './components/statistics/statistics.component';


const routes: Routes = [
  { path:'', component: OverviewComponent, canActivate: [AuthGuard] },
  { path:'login', component: LogInComponent },
  { path:'statistics', component: StatisticsComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
