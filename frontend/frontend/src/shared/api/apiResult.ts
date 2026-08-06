export type ApiResult<T> = {
    isSuccess?: boolean;
    isFailure?: boolean;
    value?: T;
    errors?: string[];
};

export function unwrapApiResult<T>(data: T | ApiResult<T>): T {
    if (typeof data === "object" && data !== null && "value" in data) {
        if (data.value === undefined || data.value === null) {
            throw new Error("API response did not include a value.");
        }

        return data.value;
    }

    return data;
}
