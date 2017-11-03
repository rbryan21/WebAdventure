import { MyGamesHomeComponent } from './home/my-games-home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MyGamesComponent } from './my-games.component';

// path's after /my-games/
const routes: Routes = [
    { 
        path: '', 
        component: MyGamesComponent,
        children: [
            { 
                path: 'create',
                loadChildren: 'app/my-games/create/create.module#CreateModule'
            },
            {
                path: '**',
                component: MyGamesHomeComponent 
            }
        ]
    }
];

@NgModule({
    imports: [
      RouterModule.forChild(routes)
    ],
    providers: [ ],
    exports: [ RouterModule ]
})
export class MyGamesRoutingModule {
    static components = [ MyGamesComponent, MyGamesHomeComponent ];
 }
