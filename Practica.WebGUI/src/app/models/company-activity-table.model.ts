import { Moment } from "moment";

export class CompanyActivityTable {
    Id: string
    Title: string
    Type: string
    StartDate: Moment
    EndDate: Moment
    PublishDate: Moment
    ExpirationDate: Moment
    City: string
    NrAplications: number
}