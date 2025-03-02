import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category/category.service';
import { Category } from '../../interfaces/category';

@Component({
  selector: 'app-category',
  standalone: false,
  
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent implements OnInit {
  newCategory : Category = {categoryId : 0, categoryName : ''};

  constructor(private categoryService: CategoryService){}

  ngOnInit() {
    this.fetchCategory();
  }

  fetchCategory() {
    this.categoryService.fetchCategory()
      .subscribe({next: (response) => {
        this.newCategory = {
          categoryId : (response as any).categoryId,
          categoryName : (response as any).categoryName
        };
        console.log(this.newCategory)
      },
      error: (error) => {
        console.error(error);
      }
  })
  };

}






