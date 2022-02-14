import { CommentProduct, PagePriceProduct, PageProduct } from './../../../../services/constants.service';
import { Component, OnInit } from '@angular/core';
import { ConstantsService} from '../../../../services/constants.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrencyService } from '../../../../services/currency.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product: PageProduct = new PageProduct();
  pageCommentProduct: CommentProduct[]=[];
  pagePriceProduct: PagePriceProduct[]=[];

  constructor(private constantsService: ConstantsService, private route: ActivatedRoute, private currencyService:CurrencyService, private router: Router) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const productIdFromRoute = String(routeParams.get('Id'));
    this.currencyService.getProductById(productIdFromRoute)
      .subscribe((data: any) =>
      {
        this.pagePriceProduct =data["priceResult"];
        // this.pageCommentProduct =data["commentsResult"];
        this.product = data["productsResult"];

        for (let i = 0; i < data["commentsResult"].length; i++) {
          this.pageCommentProduct.push({
            id: data["commentsResult"][i].id,
            productId: data["commentsResult"][i].productId,
            departureDate: data["commentsResult"][i].departureDate.split('T')[0]+ ' ' + data["commentsResult"][i].departureDate.split('T')[1],
            userId: data["commentsResult"][i].userId,
            userName: data["commentsResult"][i].userName,
            text: data["commentsResult"][i].text,
          });
        }
      });
  }

  addComment(){

  }

  routeGroup(id: string){
    let  urlId = btoa(id);
    this.router.navigate(['/menu'], { queryParams: { id: urlId } });
  }

  routeShop(id: string){
    let  urlId = btoa(id);
    this.router.navigate(['/shop/' + urlId]);
  }

}
