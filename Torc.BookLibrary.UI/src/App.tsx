import { useState } from 'react';
import { Container, Typography, CircularProgress, Alert } from '@mui/material';
import BookSearch from './components/BookSearch';
import BookList from './components/BookList';
import { fetchBooks } from './api/BookApi';
import type { BookSearchParams } from './api/BookApi';
import type { Book } from './types/Book';
import './App.css';

function App() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSearch = async (params: BookSearchParams) => {
    setLoading(true);
    setError(null);
    try {
      const data = await fetchBooks(params);
      setBooks(data);
    } catch {
      setError('Failed to fetch books');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>
        Royal Library
      </Typography>
      <BookSearch onSearch={handleSearch} />
      {loading && <CircularProgress sx={{ mt: 2 }} />}
      {error && <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>}
      <BookList books={books} />
    </Container>
  );
}

export default App;