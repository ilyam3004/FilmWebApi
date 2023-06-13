import {Component, EventEmitter, Input, Output} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";


@Component({
  selector: 'modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {
  watchlistName: string = "";
  @Output() valueChange = new EventEmitter<string>();

  constructor(private modalService: NgbModal){ }

  sendValueAndCloseModal(modal: any) {
    this.valueChange.emit(this.watchlistName);
    modal.close();
  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
}
