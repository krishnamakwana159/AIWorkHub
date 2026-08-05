import { enqueueSnackbar } from "notistack";

export function useAppSnackbar() {

  return {

    success(message: string) {
      enqueueSnackbar(message, {
        variant: "success"
      });
    },

    error(message: string) {
      enqueueSnackbar(message, {
        variant: "error"
      });
    },

    warning(message: string) {
      enqueueSnackbar(message, {
        variant: "warning"
      });
    },

    info(message: string) {
      enqueueSnackbar(message, {
        variant: "info"
      });
    }
  };

}
