import {ChangeDetectionStrategy,Component,signal,} from '@angular/core';
import {AbstractControl,FormControl,FormGroup,ReactiveFormsModule,ValidationErrors,ValidatorFn,Validators,} from '@angular/forms';
import { RouterLink } from '@angular/router';

const passwordsMatchValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const password = control.get('password')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;

  if (!password || !confirmPassword) {
    return null;
  }

  return password === confirmPassword
    ? null
    : { passwordsMismatch: true };
};

@Component({
  imports: [ReactiveFormsModule,
    RouterLink,],
  selector: 'app-reset-password',
  styleUrl: './reset-password.scss',
  templateUrl: './reset-password.html',
})
export class ResetPassword {
  readonly isSubmitting = signal(false);
  readonly isSuccess = signal(false);

  readonly showPassword = signal(false);
  readonly showConfirmPassword = signal(false);

  readonly resetPasswordForm = new FormGroup(
    {
      password: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      confirmPassword: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
    },
    {
      validators: passwordsMatchValidator,
    }
  );

  get passwordControl(): FormControl<string> {
    return this.resetPasswordForm.controls.password;
  }

  get confirmPasswordControl(): FormControl<string> {
    return this.resetPasswordForm.controls.confirmPassword;
  }

  onSubmit(): void {
    if (this.resetPasswordForm.invalid) {
      this.resetPasswordForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.isSuccess.set(false);

    // Temporary UI-only simulation.
    // Real reset-password API integration will be implemented
    // in Phase 3 according to the backend contract.
    setTimeout(() => {
      this.isSubmitting.set(false);
      this.isSuccess.set(true);
    }, 1200);
  }

  togglePasswordVisibility(): void {
    this.showPassword.update(value => !value);
  }

  toggleConfirmPasswordVisibility(): void {
    this.showConfirmPassword.update(value => !value);
  }
}
