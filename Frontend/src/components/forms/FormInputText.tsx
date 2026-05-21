import { Controller, type Control } from 'react-hook-form';
import TextField, { type OutlinedTextFieldProps } from '@mui/material/TextField';

export type FormInputTextProps = {
  name: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  control: Control<any>;
  label: string;
} & Omit<OutlinedTextFieldProps, 'name' | 'label' | 'variant'>;

export const FormInputText = ({ name, control, label, ...props }: FormInputTextProps) => {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field: { onChange, value }, fieldState: { error } }) => (
        <TextField
          helperText={error ? error.message : null}
          error={!!error}
          onChange={onChange}
          value={value || ''}
          fullWidth
          label={label}
          variant="outlined"
          {...props}
        />
      )}
    />
  );
};
