import type { Book } from '../types/Book';

export interface BookSearchParams {
  author?: string;
  isbn?: string;
  ownershipStatus?: number;
  page?: number;
  pageSize?: number;
}

const API_BASE_URL = 'http://localhost:8010/api/Books';

export async function fetchBooks(params: BookSearchParams): Promise<Book[]> {
  const query = new URLSearchParams();
  if (params.author) query.append('author', params.author);
  if (params.isbn) query.append('isbn', params.isbn);
  if (params.ownershipStatus !== undefined && params.ownershipStatus !== null)
    query.append('ownershipStatus', params.ownershipStatus.toString());
  if (params.page) query.append('page', params.page.toString());
  if (params.pageSize) query.append('pageSize', params.pageSize.toString());

  const response = await fetch(`${API_BASE_URL}?${query.toString()}`);
  if (!response.ok) throw new Error('Failed to fetch books');
  return response.json();
}