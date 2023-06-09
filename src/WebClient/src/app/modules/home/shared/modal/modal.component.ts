import {Component, Input} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {WatchlistService} from "../../../../core/services/watchlist.service";

@Component({
  selector: 'modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {
  watchlistName: string = "";
  
  constructor(private modalService: NgbModal,
              private watchlistService: WatchlistService){ }

  createWatchlist(): void{
    console.log(this.watchlistName);
  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
}
