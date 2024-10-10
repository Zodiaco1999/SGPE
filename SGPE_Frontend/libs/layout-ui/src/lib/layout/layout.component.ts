import { Component, AfterViewInit } from '@angular/core';

@Component({
  selector: 'sgpe-ws-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent implements AfterViewInit {
  async ngAfterViewInit() {
    const scripts: Script[] = [
      { id: 'menu', path: '../assets/libs/metismenu/metisMenu.min.js' },
      { id: 'pace', path: '../assets/libs/pace-js/pace.min.js' },
      { id: 'app', path: '../assets/js/app.js' },
    ];

    for (const s of scripts) {
      document.getElementById(s.id)?.remove();
      this.loadScript(s);
      //console.log('loaded', script);
    }
  }

  loadScript(s: Script) {
    return new Promise((resolve, reject) => {
      // create JS library script element
      const script = document.createElement('script');
      script.id = s.id;
      script.src = s.path;
      //console.log(s.id + ' created');

      document.body.appendChild(script);

      // if the script returns okay, return resolve
      script.onload = () => resolve(s.id);

      // if it fails, return reject
      script.onerror = () => {
        console.log(s.id + ' load failed');
        return reject(s.id);
      };
    });
  }
}

type Script = {
  id: string;
  path: string;
};
