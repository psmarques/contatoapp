import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SocialUser } from 'angularx-social-login';
import { Contato } from '../../models/contato.model';
import { contatoService } from '../../services/contato.service';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public errorMessage: String;
  public contatos: Contato[] = [];
  public contato: Contato = new Contato('', '', '', '', null, null);;
  public form: FormGroup;
  public modo: String = "List";
  public success: Boolean = false;
  public versao: String = "1.0.1";
  public user: SocialUser;

  id: String;

  constructor(private fb: FormBuilder, private apiContato: contatoService, private session: SessionService) {

    this.form = this.fb.group({
      id: [],
      primeiroNome: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      ultimoNome: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      email: ['', Validators.compose([Validators.required, Validators.minLength(5)])],
      telefoneDDD: ['', Validators.compose([Validators.min(1), Validators.max(99)])],
      telefoneNumero: ['', Validators.compose([Validators.min(10000000), Validators.max(999999999)])]
    });

  }

  ngOnInit(): void {
    //throw new Error('Method not implemented.');
    this.user = this.session.getUser();

    this.session.userObservable.subscribe(val => {

      if (val == null) return;
      this.user = val as SocialUser;

      this.loadContatos();
    });

    if (this.user != null)
      this.loadContatos();
  }

  ngOnDestroy(): void {
    this.session.userObservable.unsubscribe();
  }

  private loadContatos() {

    this.user = this.session.getUser();

    if (this.user == null)
      return;

    this.apiContato.getAll().subscribe(result => {
      console.log(result);
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
