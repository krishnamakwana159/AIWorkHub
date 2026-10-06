export interface GenerateDescriptionRequest {
    title: string;
}

export interface GenerateDescriptionResponse {
    description: string;
}

export interface GenerateBreakdownRequest {
    title: string;
    description?: string;
}

export interface SuggestPriorityRequest {
    title: string;
    description?: string;
}
