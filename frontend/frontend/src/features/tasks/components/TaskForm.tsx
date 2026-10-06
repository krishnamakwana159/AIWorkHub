import { Button, Stack } from "@mui/material";
import AutoAwesomeIcon from "@mui/icons-material/AutoAwesome";

import { FormProvider, useForm } from "react-hook-form";

import { zodResolver } from "@hookform/resolvers/zod";

import FormSelect from "@/shared/forms/FormSelect";
import FormTextField from "@/shared/forms/FormTextField";

import { TaskPriority, TaskPriorityInfo } from "@/shared/constants/task";

import { useGenerateTaskDescription } from "@/features/ai/hooks/useGenerateTaskDescription";
import { useSuggestTaskPriority } from "@/features/ai/hooks/useSuggestTaskPriority";

import { taskSchema, type TaskFormValues } from "../schema/taskSchema";

type Props = {
  defaultValues: TaskFormValues;
  onSubmit(values: TaskFormValues): void;
};

const PRIORITY_NAME_TO_VALUE: Record<string, TaskPriority> = {
  low: TaskPriority.Low,
  medium: TaskPriority.Medium,
  high: TaskPriority.High,
  critical: TaskPriority.Critical,
};

export default function TaskForm({ defaultValues, onSubmit }: Props) {
  const methods = useForm<TaskFormValues>({
    resolver: zodResolver(taskSchema),
    defaultValues,
  });

  const generateDescription = useGenerateTaskDescription();
  const suggestPriority = useSuggestTaskPriority();

  const title = methods.watch("title");

  async function handleGenerateDescription() {
    if (!title?.trim()) return;

    const description = await generateDescription.mutateAsync({ title });
    methods.setValue("description", description, { shouldDirty: true });
  }

  async function handleSuggestPriority() {
    if (!title?.trim()) return;

    const description = methods.getValues("description");
    const suggested = await suggestPriority.mutateAsync({
      title,
      description,
    });

    const normalized = suggested.trim().toLowerCase();
    const matched = Object.keys(PRIORITY_NAME_TO_VALUE).find((key) =>
      normalized.includes(key)
    );

    if (matched) {
      methods.setValue("priority", PRIORITY_NAME_TO_VALUE[matched], {
        shouldDirty: true,
      });
    }
  }

  return (
    <FormProvider {...methods}>
      <form id="task-form" onSubmit={methods.handleSubmit(onSubmit)}>
        <Stack spacing={3}>
          <FormTextField name="title" label="Task Title" />

          <Stack spacing={1}>
            <FormTextField
              name="description"
              label="Description"
              multiline
              rows={4}
            />

            <Button
              size="small"
              startIcon={<AutoAwesomeIcon fontSize="small" />}
              disabled={!title?.trim() || generateDescription.isPending}
              onClick={handleGenerateDescription}
              sx={{ alignSelf: "flex-start" }}
            >
              Generate with AI
            </Button>
          </Stack>

          <Stack spacing={1}>
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

            <Button
              size="small"
              startIcon={<AutoAwesomeIcon fontSize="small" />}
              disabled={!title?.trim() || suggestPriority.isPending}
              onClick={handleSuggestPriority}
              sx={{ alignSelf: "flex-start" }}
            >
              Suggest Priority with AI
            </Button>
          </Stack>

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
