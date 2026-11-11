import { test, expect } from '@playwright/test';

test('shows High Blood Pressure for a reading of 150/100', async ({ page }) => {
  await page.goto('/');

  await page.getByLabel('Systolic').fill('150');
  await page.getByLabel('Diastolic').fill('100');
  await page.getByRole('button', { name: 'Submit' }).click();

  await expect(page.getByText('High Blood Pressure', { exact: true })).toBeVisible();
  await expect(page.getByText('MAP: 117 mmHg')).toBeVisible();
});

test('rejects a reading where systolic is not higher than diastolic', async ({ page }) => {
  await page.goto('/');

  await page.getByLabel('Systolic').fill('80');
  await page.getByLabel('Diastolic').fill('85');
  await page.getByRole('button', { name: 'Submit' }).click();

  await expect(page.getByText('Systolic must be greater than Diastolic')).toBeVisible();
});