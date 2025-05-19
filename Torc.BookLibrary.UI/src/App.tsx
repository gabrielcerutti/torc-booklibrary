import { useEffect, useState } from 'react';
import { Container, Typography, Alert, Box } from '@mui/material';
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

  // Initial fetch to load all books
  useEffect(() => {
    const fetchAllBooks = async () => {
      setLoading(true);
      setError(null);
      try {
        const data = await fetchBooks({}); // Fetch all books
        setBooks(data);
      } catch {
        setError('Failed to fetch books');
      } finally {
        setLoading(false);
      }
    };
    fetchAllBooks();
  }, []);

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
    <Container maxWidth="lg" sx={{
      display: 'flex',
      flexDirection: 'column',
      minHeight: '100vh',
      py: 2
    }}>
      <Typography variant="h4" gutterBottom>
        Royal Library
      </Typography>
      {/* BookSearch as header */}
      <Box sx={{ mb: 2 }}>
        <BookSearch onSearch={handleSearch} />
      </Box>
      {/* BookList as content body, fills remaining space */}
      <Box sx={{ flex: 1, display: 'flex', flexDirection: 'column', minHeight: 0 }}>        
        {error && <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>}
        <Box sx={{ flex: 1, overflow: 'auto' }}>
          <BookList books={books} loading={loading} />
        </Box>
      </Box>
    </Container>
  );
}

export default App;