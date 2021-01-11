import { Component } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Contato } from '../../models/contato.model';
import { contatoService } from '../../services/contato.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public errorMessage: String;
  public contatos: Contato[] = [];
  public contato: Contato = new Contato('', '', '', '', null, null);;
  public form: FormGroup;
  public modo: String = "List";
  public success: Boolean = false;

  id: String;

  constructor(private fb: FormBuilder, private apiContato: contatoService) {

    this.form = this.fb.group({
      id: [],
      primeiroNome: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      ultimoNome: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      email: ['', Validators.compose([Validators.required, Validators.minLength(5)])],
      telefoneDDD: ['', Validators.compose([Validators.min(1), Validators.max(99)])],
      telefoneNumero: ['', Validators.compose([Validators.min(10000000), Validators.max(999999999)])]
    });

    this.loadContatos();
  }

  private loadContatos() {

    this.apiContato.getAll().subscribe(result => {
      this.contatos = result;
    },
      error => {
        console.log(error);
        this.exibirAlerta(error.message, false);
      });
  }

  remove(item: Contato) {

    this.apiContato.delete(item.id).subscribe(result => {
      this.exibirAlerta(result.message, result.success);
      this.loadContatos();
    },
      error => {
        console.log(error);
        this.exibirAlerta(error.message, false);
      });
  }

  salvar() {
    this.contato = new Contato(this.form.controls.id.value, this.form.controls.primeiroNome.value, this.form.controls.ultimoNome.value, this.form.controls.email.value, this.form.controls.telefoneDDD.value, this.form.controls.telefoneNumero.value);

    if (this.id) {

      this.apiContato.update(this.contato).subscribe(
        result => {

          this.exibirAlerta(result.message, result.success);

          if (result.success) {
            this.form.reset();
            this.id = null;
            this.alterarModo();
            this.loadContatos();
          }
        },
        error => {
          this.exibirAlerta(error.message, false);
          console.log(error);
        });

    }
    else {

      this.apiContato.post(this.contato).subscribe(
        result => {

          this.exibirAlerta(result.message, result.success);

          if (result.success) {
            this.form.reset();
            this.alterarModo();
            this.loadContatos();
          }
        },
        error => {
          this.exibirAlerta(error.message, false);
          console.log(error);
        });
    }
  }

  voltar() {
    this.id = null;
    this.alterarModo();
  }

  exibirAlerta(msg: String, success: Boolean) {
    this.errorMessage = msg;
    this.success = success;

    setTimeout(() => {
      this.errorMessage = null;
    }, 5000);
  }

  alterarModo() {
    this.modo = this.modo == 'Novo' ? this.id != undefined ? 'Edit' : 'List' : 'Novo';

    if (this.id == null)
      this.form.reset();
  }

  edit(item: Contato) {
    this.id = item.id;
    this.form.controls.id.setValue(item.id);
    this.form.controls.primeiroNome.setValue(item.primeiroNome);
    this.form.controls.ultimoNome.setValue(item.ultimoNome);
    this.form.controls.email.setValue(item.email);
    this.form.controls.telefoneDDD.setValue(item.telefoneDDD.toString());
    this.form.controls.telefoneNumero.setValue(item.telefoneNumero.toString());

    this.alterarModo();

    this.contato = item;
  }
}
