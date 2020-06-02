import {Component, HostListener, OnInit, ViewChild, ElementRef, ViewChildren} from '@angular/core';
import {Subject} from 'rxjs';
import { postgreSqlKeyWords } from './dictionary';
import { RunScriptService } from './runscript.service';
import { Script } from './script.model';
import {MatDialog} from '@angular/material/dialog';
import {ConnectionDialogComponent} from '../connectionDialog/connDiag.component';

@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.css'],
  providers: [RunScriptService]
})
export class WorkspaceComponent implements OnInit {

  @ViewChildren('script-card') card: ElementRef;
  textInput: Subject<string> = new Subject<string>();
  keyInput: Subject<string> = new Subject<string>();
  isConnected = true;
  selected = 'None';
  connStr = 'Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;';
  script = '';
  input = '';

  constructor(private runScriptService: RunScriptService, public dialog: MatDialog) {
  }

  ngOnInit() {
    this.textInput.subscribe(letter => {
      this.input += letter;
      this.showScript();
    });
    this.keyInput.subscribe(key => {
      if (key === '`') {
        console.log(`${this.script.indexOf('<br>', -1)} === ${this.script.length - 1}`);
        if (this.script.indexOf('<br>', -1) === this.script.length - 4) {
          this.script = this.script.slice(0, -4);
          this.input = this.input.slice(0, -1);

        } else {
          this.script = this.script.slice(0, -1);
          this.input = this.input.slice(0, -1);
        }
      }
      if (/Enter/.test(key)) {
        this.script += '<br>';
      }
      this.showScript();
    });
  }


  @HostListener('document:keyup', ['$event'])
  onKeyUp(ev: KeyboardEvent) {
    if (/^[A-Za-z0-9*;(),<>'-+.!@#$%^&=" ]$/.test(ev.key)) {
      this.textInput.next(ev.key);
    } else {
      this.keyInput.next(ev.key);
    }
  }


  showScript() {
    this.script = `<p class="script">${
      (this.input.split(' ').map<string>((word) => {
        if (postgreSqlKeyWords.includes(word.toUpperCase())) {
          return `<span class="keyWord">${word.toUpperCase()}</span>`;
        } else {

          return word;
        }
      })).join(' ')
    }
      </p>`;
  }

  onRun() {
    const script: Script = {
      DbType: this.selected,
      DbScript: this.input,
      ConStr: this.connStr,
      User: {
        Id: 1,
        Name: 'Vlad',
        AccType: 'moderator'
    }
    };
    console.log(script.DbScript)
    this.runScriptService.runScript(script);
  }


  openDialog(): void {

    const dialogRef = this.dialog.open(ConnectionDialogComponent, {
      width: '600px',
      data: {connectionString: this.connStr }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      this.connStr = result.connectionString;
      this.isConnected = result.isConnected;
    });
    this.isConnected = true;
  }

}

