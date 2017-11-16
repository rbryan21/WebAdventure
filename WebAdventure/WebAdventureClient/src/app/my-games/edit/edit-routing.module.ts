import { EditGuard } from './../../core/services/guards/edit-guard.service';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CanDeactivateGuard } from './../../core/services/guards/can-deactivate-guard.service';

import { EditComponent } from './edit.component';
import { GameInfoComponent } from './game-info/game-info.component';
import { GameInfoResolver } from '../../core/services/resolvers/games/edit/game-info-resolver.service';

// path's after /create/
const routes: Routes = [
    { 
        path: '', 
        component: EditComponent,
        canActivate: [ EditGuard ],
        children: [
            { 
                path: 'rooms',
                loadChildren: 'app/my-games/edit/rooms/rooms.module#RoomsModule'
            },
            {
                path: '**', 
                component: GameInfoComponent, // redirect all other paths to create info
                canDeactivate: [CanDeactivateGuard],
                resolve: {
                    gameResponse: GameInfoResolver 
                }
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
export class EditRoutingModule {
    static components = [ EditComponent, GameInfoComponent ];
 }
