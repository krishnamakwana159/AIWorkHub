export type ApiResult<T> = {
    isSuccess?: boolean;
    isFailure?: boolean;
    value?: T;
    errors?: string[];
};

function isApiResultWrapper<T>(data: unknown): data is ApiResult<T> {
    return (
        typeof data === "object" &&
        data !== null &&
        "value" in data &&
        ("isSuccess" in data || "isFailure" in data)
    );
}

export function unwrapApiResult<T>(data: T | ApiResult<T>): T {
    if (isApiResultWrapper<T>(data)) {
        if (data.value === undefined || data.value === null) {
            throw new Error("API response did not include a value.");
        }

        return data.value;
    }

    return data as T;
}
