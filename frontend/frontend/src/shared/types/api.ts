export interface ApiResponse<T> {
    succeeded: boolean;
    message: string;
    data: T;
}

export interface PagedResult<T> {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
}
