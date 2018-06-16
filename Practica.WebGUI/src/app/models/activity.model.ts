import { Moment } from "moment";

export class Activity {
    Id: string
    Title: string
    Type: string
    Description: string
    StartDate: Moment
    EndDate: Moment
    PublishDate: Moment
    ExpirationDate: Moment
    Country: string
    City: string
    Address: string
}