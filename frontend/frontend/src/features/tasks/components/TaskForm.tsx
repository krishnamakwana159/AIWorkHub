import { Stack } from "@mui/material";

import { FormProvider, useForm } from "react-hook-form";

import { zodResolver } from "@hookform/resolvers/zod";

import FormSelect from "@/shared/forms/FormSelect";
import FormTextField from "@/shared/forms/FormTextField";

import { TaskPriority, TaskPriorityInfo } from "@/shared/constants/task";

import { taskSchema, type TaskFormValues } from "../schema/taskSchema";

type Props = {
  defaultValues: TaskFormValues;
  onSubmit(values: TaskFormValues): void;
};

export default function TaskForm({ defaultValues, onSubmit }: Props) {
  const methods = useForm<TaskFormValues>({
    resolver: zodResolver(taskSchema),
    defaultValues,
  });

  return (
    <FormProvider {...methods}>
      <form id="task-form" onSubmit={methods.handleSubmit(onSubmit)}>
        <Stack spacing={3}>
          <FormTextField name="title" label="Task Title" />

          <FormTextField
            name="description"
            label="Description"
            multiline
            rows={4}
          />

          <FormSelect
            name="priority"
            label="Priority"
            options={Object.values(TaskPriority)
              .filter((value) => typeof value === "number")
              .map((priority) => ({
                value: priority,
                label: TaskPriorityInfo[priority].label,
              }))}
          />

          <FormTextField
            name="estimatedHours"
            label="Estimated Hours"
            type="number"
          />

          <FormTextField name="startDateUtc" label="Start Date" type="date" />

          <FormTextField name="dueDateUtc" label="Due Date" type="date" />
        </Stack>
      </form>
    </FormProvider>
  );
}
