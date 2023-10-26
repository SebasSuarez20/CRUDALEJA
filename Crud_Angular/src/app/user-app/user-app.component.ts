import { Component, OnInit } from '@angular/core';
import { ServiceAppService } from '../services/service-app.service';
import Swal from 'sweetalert2';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, flatMap } from 'rxjs';


@Component({
  selector: 'app-user-app',
  templateUrl: './user-app.component.html',
  styleUrls: ['./user-app.component.css']
})
export class UserAppComponent implements OnInit {


  public Data: any[] = [];
  public UltimeData: any[] = [];
  public cantMin = 0;
  public cantMax = 4;
  public LenMax = 0;
  public FormUser = new FormGroup({
    idControl: new FormControl(null),
    name: new FormControl('', Validators.required),
    surname: new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    age: new FormControl('', Validators.required),
    phone: new FormControl('', Validators.required),
    enabled: new FormControl(true)
  })
  public InfoLeng = new BehaviorSubject<any>({});
  public DisabledNext: boolean = false;
  public DisabledPrevious: boolean = false;

  public SendPost: boolean = true;

  constructor(private service: ServiceAppService) { }

  ngOnInit(): void {
    this.GetAllUser(true);
  }

  public GetAllUser(isLoad: boolean) {
    let aux: any = [];
    this.service.GetAll('GetUser').subscribe({
      next: Response => {
        this.LenMax = Response.items.length;
        console.log(Response)
        this.UltimeData = Response.items.at(-2).idControl;
        for (let index = 0; index < Response.items.length; ++index) {
          if (index >= this.cantMin && index <= this.cantMax) {
            aux.push(Response.items[index]);
          }
        }
        this.Data = aux;
        console.log(this.Data);

        if (isLoad == true) {
          this.InfoLeng.next(this.LenMax);
        }

      }, error: Error => {
        console.error(Error);
      }

    })
  }

  public DeleteAllUser(id: number): void {

    Swal.fire({
      text: "¿Deseas eliminar el Item?",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      toast: true
    }).then((result) => {
      if (result.isConfirmed) {


        this.service.DeleteAll(id).subscribe(({
          next: Response => {
            Swal.fire({
              icon: 'success',
              text: "Se elimino correctamente.",
              timer: 2200,
              timerProgressBar: true,
              showCancelButton: false,
              showConfirmButton: false,
              toast: true
            }).then(() => {
              this.cantMax = 4;
              this.cantMin = 0;
              this.GetAllUser(false);

            })
          }, error: Error => {

            Swal.fire({
              icon: 'error',
              text: 'No se pudo eliminar el usuario.',
              toast: true,
              showCancelButton: false,
              showConfirmButton: false,
              timerProgressBar: true,
              timer: 1200
            })

          }
        }))

      }
    })

  }

  public NextPage() {

    if (this.cantMax < this.LenMax - 1) { //
      this.cantMax++;
      this.cantMin++;
      this.GetAllUser(false);
    }
  }

  public PreviousPage() {
    if (this.cantMin != 0) {
      this.cantMin--;
      this.cantMax--;
      this.GetAllUser(false);
    }

  }

  public PostAllUser() {
    this.service.PostAll(this.FormUser.value).subscribe({
      next: Response => {
        this.GetAllUser(false);
        this.FormUser.reset();
        this.SendPost = true;
      }, error: Error => {
        Swal.fire({
          icon: 'error',
          text: 'No se pudo crear el usuario.',
          toast: true,
          showCancelButton: false,
          showConfirmButton: false,
          timerProgressBar: true,
          timer: 1200
        })
      }
    })
  }

  public PutAllUser() {
    this.service.PutAll(this.FormUser.value).subscribe(({
      next: Response => {
        this.GetAllUser(false);

        this.FormUser.reset();
        this.SendPost = true;
      }, error: Error => {
        console.error(Error);
      }
    }))
  }

  public GetFieldsForPUT(item: any): void {
    console.log(item)
    this.FormUser.patchValue(item);
    debugger;
    this.SendPost = false;
  }

  public Onsubmit() {

    if (this.FormUser.valid) {
      if (this.SendPost == true) {
        // Se reinicia la variable del formulario;
        this.FormUser.get('enabled')?.setValue(true);
        this.PostAllUser();
      } else {
        this.PutAllUser();
      }

    } else {
      Swal.fire({
        icon: 'error',
        text: 'Hay campos que estan invalidos,revisa.',
        toast: true,
        showCancelButton: false,
        showConfirmButton: false,
        timerProgressBar: true,
        timer: 2200
      })
    }

  }



}
