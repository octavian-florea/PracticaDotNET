export interface IActivity {
    id: number;
    title: string;
    description: string;
    startDate: Date;
    endDate: Date;
    aplicationEndDate: Date;
    type: string;
    city: string;
    address: string;
    seats: number;
}