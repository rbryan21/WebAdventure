import { IRoom } from './../../models/room.interface';
import { IResponse } from './../response.interface';
import { IItem } from '../../models/item.interface';

export interface IRoomItemsResponse extends IResponse {
    items: IItem[];
}
