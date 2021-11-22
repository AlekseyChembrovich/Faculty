import {Component, OnInit} from "@angular/core";
import {CuratorModel} from "../models/curator.model";
import {CuratorsService} from "../services/curator.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-curator-delete',
  templateUrl: './curator.delete.component.html'
})
export class CuratorDeleteComponent implements OnInit {
  public curator: CuratorModel | undefined;

  constructor(private curatorsService: CuratorsService,
              private activatedRoute: ActivatedRoute,
              private router: Router)
  {
  }

  ngOnInit() : void {
    let id = 0;
    this.activatedRoute.params.subscribe(params => {
      console.log(params);
      id = params['id'];
    });

    this.curatorsService.getCurator(id).subscribe(response => {
      this.curator = response;
    });
  }

  public delete() : void {
    let id = this.curator?.id;
    if (id === undefined) return;
    this.curatorsService.deleteCurator(id).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/curator/index');
    });
  }
}
