import { Component, OnInit, ViewChild } from '@angular/core';
import { Story } from '../../Models/story.model';
import { StoriesService } from '../../Services/stories.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-stories-list',
  templateUrl: './stories-list.component.html',
  styleUrls: ['./stories-list.component.css']
})

export class StoriesListComponent implements OnInit {
  
  stories: Story[] = [];
  dataSource: any;
  title = 'titulo';
  page: number = 1;
  count: number = 0;
  tableSize: number = 10;
  tableSizes: any = [5, 10, 25, 50];
  columnNames: string[] = ['ID', 'Title', 'Link'];
  loading: boolean = false;

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  constructor(private storiesService: StoriesService) { }

  ngOnInit(): void {
    this.loading = true;
    this.getStories();
    
  }

  getStories(): void {
    this.storiesService.getAllStories()
      .subscribe({
        next: (stories) => {
          this.stories = stories
          this.dataSource = new MatTableDataSource<Story>(this.stories);
          this.dataSource.paginator = this.paginator;
          this.loading = false; 
        },
        error: (response) => {
          console.log(response)
        }
      })
  }
}

