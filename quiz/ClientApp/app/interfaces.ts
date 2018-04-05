import { } from 'signalr';

/* SignalR related interfaces  */
export interface FeedSignalR extends SignalR {
    broadcaster: FeedProxy
}

export interface FeedProxy {
    client: FeedClient;
    server: FeedServer;
}

export interface FeedClient {
    setConnectionId: (id: string) => void;
    //updateMatch: (match: Match) => void;
    addFeed: (feed: Feed) => void;
    addActionMessage: (actionMessage: ActionMessage) => void;
}

export interface FeedServer {
    subscribe(matchId: number): void;
    unsubscribe(matchId: number): void;
}

export enum SignalRConnectionStatus {
    Connected = 1,
    Disconnected = 2,
    Error = 3
}

export interface Feed {
    Id: number;
    Description: string;
    CreatedAt: Date;
    MatchId: number;
}
export interface ActionMessage {
    MatchId: number;
    Action: string;
    ObjectId: string;
    CreatedAt: Date;
}

/*
export interface Match {
    Id: number;
    Host: string;
    Guest: string;
    HostScore: number;
    GuestScore: number;
    MatchDate: Date;
    Type: string;
    Feeds: Feed[];
}



export interface ChatMessage {
    MatchId: number;
    Text: string;
    CreatedAt: Date;
}
*/