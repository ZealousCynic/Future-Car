import { Component, OnInit } from '@angular/core';
import { StatisticsService } from 'src/app/statistics.service';
import { Statistics } from 'src/app/statistics';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {
  statistics: Statistics;

  constructor(private statisticsService: StatisticsService) { }
  
  ngOnInit() {
    this.statisticsService.getVortexIdByUserId();
  }
}
