import React, { useState } from 'react';
import { TextField, Button, MenuItem, Box, Grid } from '@mui/material';
import type { BookSearchParams } from '../api/BookApi';

type SearchBy = 'author' | 'isbn' | 'ownershipStatus';

interface BookSearchProps {
  onSearch: (params: BookSearchParams) => void;
}

const BookSearch: React.FC<BookSearchProps> = ({ onSearch }) => {
  const [searchBy, setSearchBy] = useState<SearchBy>('author');
  const [author, setAuthor] = useState('');
  const [isbn, setIsbn] = useState('');
  const [ownershipStatus, setOwnershipStatus] = useState<number | ''>('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const params: BookSearchParams = {};
    if (searchBy === 'author') params.author = author;
    if (searchBy === 'isbn') params.isbn = isbn;
    if (searchBy === 'ownershipStatus' && ownershipStatus !== '') params.ownershipStatus = Number(ownershipStatus);
    onSearch(params);
  };

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ mb: 2, flexGrow: 1 }}>
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, md: 3 }}>
          <TextField
            label="Search By"
            select
            value={searchBy}
            onChange={e => setSearchBy(e.target.value as SearchBy)}
            fullWidth
            variant="outlined"
            size="small"
          >
            <MenuItem value="author">Author</MenuItem>
            <MenuItem value="isbn">ISBN</MenuItem>
            <MenuItem value="ownershipStatus">Ownership Status</MenuItem>
          </TextField>
        </Grid>
        {searchBy === 'author' && (
          <Grid size={{ xs: 12, md: 3 }}>
            <TextField
              label="Author"
              value={author}
              onChange={e => setAuthor(e.target.value)}
              fullWidth
              variant="outlined"
              size="small"
            />
          </Grid>
        )}
        {searchBy === 'isbn' && (
          <Grid size={{ xs: 12, md: 3 }}>
            <TextField
              label="ISBN"
              value={isbn}
              onChange={e => setIsbn(e.target.value)}
              fullWidth
              variant="outlined"
              size="small"
            />
          </Grid>
        )}
        {searchBy === 'ownershipStatus' && (
          <Grid size={{ xs: 12, md: 3 }}>
            <TextField
              label="Ownership Status"
              select
              value={ownershipStatus}
              onChange={e => {
                const val = e.target.value;
                setOwnershipStatus(val === '' ? '' : Number(val));
              }}
              fullWidth
              variant="outlined"
              size="small"
            >
              <MenuItem value="">Any</MenuItem>
              <MenuItem value={0}>Own</MenuItem>
              <MenuItem value={1}>Love</MenuItem>
              <MenuItem value={2}>Want to Read</MenuItem>
            </TextField>
          </Grid>
        )}
        <Grid size={{ xs: 12, md: 2 }}>
          <Button type="submit" variant="contained" color="primary" fullWidth sx={{ height: '100%' }}>
            Search
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default BookSearch;