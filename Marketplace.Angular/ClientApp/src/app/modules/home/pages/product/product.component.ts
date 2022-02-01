import { Component, OnInit } from '@angular/core';
import { ConstantsService, PageCommentProduct, PageMenuProduct, Product } from '../../../../services/constants.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  product: PageMenuProduct = new PageMenuProduct();
  pageCommentProduct: PageCommentProduct[]=[];

  constructor(private constantsService: ConstantsService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const productIdFromRoute = String(routeParams.get('productId'));
    this.constantsService.definitelyThereProducts.then((t:any) =>
    {
      for (let index = 0; index < this.constantsService.pageMenuProducts.length; index++) {
        if(this.constantsService.pageMenuProducts[index].productId == productIdFromRoute){
          this.product = this.constantsService.pageMenuProducts[index];
          break;
        }
      }

      for (let index = 0; index < this.constantsService.pageCommentProduct.length; index++) {
        if(this.constantsService.pageCommentProduct[index].productId == productIdFromRoute){
          this.pageCommentProduct.push({
            productId:this.constantsService.pageCommentProduct[index].productId,
            commentId:this.constantsService.pageCommentProduct[index].commentId,
            departureDate: this.constantsService.pageCommentProduct[index].departureDate,
            userId:this.constantsService.pageCommentProduct[index].userId,
            userName:this.constantsService.pageCommentProduct[index].userName,
            text:this.constantsService.pageCommentProduct[index].text,
          });
        }
      }
      debugger;
    });

  }
}
