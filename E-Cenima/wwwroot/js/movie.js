// Movie Management System - Enhanced JavaScript Functionality

$(document).ready(function () {
    // Initialize movie management features
    initializeMovieFeatures();
    initializeImageUpload();
    initializeFormValidation();
    initializeActorSelection();
    initializeFilters();
    initializeAnimations();
});

// Main initialization function
function initializeMovieFeatures() {
    // Auto-hide alerts after 5 seconds
    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);

    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize popovers
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Add loading states to forms
    $('form').on('submit', function () {
        const submitBtn = $(this).find('button[type="submit"]');
        const originalText = submitBtn.html();
        submitBtn.prop('disabled', true);
        submitBtn.html('<i class="fas fa-spinner fa-spin me-2"></i>Processing...');

        // Re-enable after 10 seconds as fallback
        setTimeout(function () {
            submitBtn.prop('disabled', false);
            submitBtn.html(originalText);
        }, 10000);
    });
}

// Image upload functionality
function initializeImageUpload() {
    $('#imageFileInput').on('change', function () {
        const file = this.files[0];
        const preview = $('#imagePreview');

        if (file) {
            // Validate file size (5MB)
            if (file.size > 5 * 1024 * 1024) {
                showNotification('File size must be less than 5MB', 'error');
                $(this).val('');
                resetImagePreview();
                return;
            }

            // Validate file type
            const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif'];
            if (!allowedTypes.includes(file.type)) {
                showNotification('Please select a valid image file (JPG, PNG, GIF)', 'error');
                $(this).val('');
                resetImagePreview();
                return;
            }

            // Show preview
            const reader = new FileReader();
            reader.onload = function (e) {
                preview.html(`
                    <img src="${e.target.result}" alt="Preview" class="preview-image">
                    <div class="image-info">
                        <small class="text-success">New image selected:</small><br>
                        <small class="text-muted">${file.name}</small><br>
                        <small class="text-muted">${(file.size / 1024 / 1024).toFixed(2)} MB</small>
                    </div>
                `);

                // Add remove button
                preview.append(`
                    <button type="button" class="btn btn-sm btn-danger position-absolute top-0 end-0 m-2" 
                            onclick="removeSelectedImage()" style="z-index: 10;">
                        <i class="fas fa-times"></i>
                    </button>
                `);
            };
            reader.readAsDataURL(file);
        } else {
            resetImagePreview();
        }
    });
}

// Reset image preview
function resetImagePreview() {
    $('#imagePreview').html(`
        <div class="image-placeholder">
            <i class="fas fa-image fa-3x text-muted"></i>
            <p class="text-muted mt-2">No image selected</p>
        </div>
    `);
}

// Remove selected image
function removeSelectedImage() {
    $('#imageFileInput').val('');
    resetImagePreview();
}

// Form validation enhancements
function initializeFormValidation() {
    // Character counter for description
    $('#Description').on('input', function () {
        const count = $(this).val().length;
        const maxLength = 1000;
        $('#descriptionCount').text(count);

        if (count > maxLength * 0.9) {
            $('#descriptionCount').addClass('text-warning');
        } else {
            $('#descriptionCount').removeClass('text-warning');
        }

        if (count >= maxLength) {
            $('#descriptionCount').addClass('text-danger').removeClass('text-warning');
        } else {
            $('#descriptionCount').removeClass('text-danger');
        }
    });

    // Real-time validation
    $('input, textarea, select').on('blur', function () {
        validateField(this);
    });

    // Trailer URL validation
    $('#TrailerURL').on('input', function () {
        const url = $(this).val().trim();
        if (url && !isValidUrl(url)) {
            $(this).addClass('is-invalid');
            showFieldError(this, 'Please enter a valid URL');
        } else {
            $(this).removeClass('is-invalid');
            removeFieldError(this);
            if (url) {
                $(this).addClass('is-valid');
            }
        }
    });

    // Release date validation
    $('#ReleaseDate').on('change', function () {
        const selectedDate = new Date($(this).val());
        const today = new Date();
        const maxDate = new Date();
        maxDate.setFullYear(today.getFullYear() + 10);

        if (selectedDate > maxDate) {
            $(this).addClass('is-invalid');
            showFieldError(this, 'Release date cannot be more than 10 years in the future');
        } else {
            $(this).removeClass('is-invalid');
            removeFieldError(this);
            if ($(this).val()) {
                $(this).addClass('is-valid');
            }
        }
    });
}

// Field validation helper
function validateField(field) {
    const $field = $(field);
    $field.removeClass('is-invalid is-valid');
    removeFieldError(field);

    if ($field.is('[required]') && !$field.val().trim()) {
        $field.addClass('is-invalid');
        showFieldError(field, 'This field is required');
        return false;
    }

    if ($field.val().trim() && field.checkValidity()) {
        $field.addClass('is-valid');
        return true;
    } else if ($field.val().trim()) {
        $field.addClass('is-invalid');
        return false;
    }

    return true;
}

// Show field error
function showFieldError(field, message) {
    removeFieldError(field);
    $(field).after(`<div class="custom-error text-danger small mt-1">${message}</div>`);
}

// Remove field error
function removeFieldError(field) {
    $(field).siblings('.custom-error').remove();
}

// URL validation helper
function isValidUrl(string) {
    try {
        new URL(string);
        return true;
    } catch (_) {
        return false;
    }
}

// Actor selection functionality
function initializeActorSelection() {
    // Initialize selected actors on page load (for edit form)
    updateSelectedActors();

    // Actor selection change handler
    $('#actorSelect').on('change', function () {
        updateSelectedActors();
    });

    // Remove actor functionality
    $(document).on('click', '.remove-actor', function () {
        const actorId = $(this).data('actor-id');
        $(`#actorSelect option[value="${actorId}"]`).prop('selected', false);
        updateSelectedActors();
    });
}

// Update selected actors display
function updateSelectedActors() {
    const selectedOptions = $('#actorSelect option:selected');
    const container = $('#selectedActors');

    container.empty();

    if (selectedOptions.length > 0) {
        container.append('<h6 class="mb-2"><i class="fas fa-users me-2"></i>Selected Actors:</h6>');

        selectedOptions.each(function () {
            const actorId = $(this).val();
            const actorName = $(this).text();

            container.append(`
                <div class="selected-actor-item">
                    <span class="actor-name">${actorName}</span>
                    <button type="button" class="btn btn-sm btn-outline-danger remove-actor" data-actor-id="${actorId}">
                        <i class="fas fa-times"></i>
                    </button>
                    <input type="hidden" name="SelectedActorIds" value="${actorId}" />
                </div>
            `);
        });
    } else {
        container.append('<p class="text-muted small">No actors selected</p>');
    }
}

// Filter functionality
function initializeFilters() {
    // Search functionality
    $('#movieSearch').on('input', debounce(function () {
        filterMovies();
    }, 300));

    // Category filter
    $('#categoryFilter').on('change', function () {
        filterMovies();
    });

    // Year filter
    $('#yearFilter').on('change', function () {
        filterMovies();
    });

    // Clear filters
    $('#clearFilters').on('click', function () {
        $('#movieSearch').val('');
        $('#categoryFilter').val('');
        $('#yearFilter').val('');
        filterMovies();
        showNotification('Filters cleared', 'info');
    });
}

// Filter movies function
function filterMovies() {
    const searchTerm = $('#movieSearch').val().toLowerCase();
    const selectedCategory = $('#categoryFilter').val();
    const selectedYear = $('#yearFilter').val();

    let visibleCount = 0;

    $('.movie-card-wrapper').each(function () {
        const movieName = $(this).data('name') || '';
        const movieCategory = $(this).data('category') || '';
        const movieYear = $(this).data('year') ? $(this).data('year').toString() : '';

        const matchesSearch = !searchTerm || movieName.includes(searchTerm);
        const matchesCategory = !selectedCategory || movieCategory === selectedCategory;
        const matchesYear = !selectedYear || movieYear === selectedYear;

        if (matchesSearch && matchesCategory && matchesYear) {
            $(this).show().addClass('fade-in');
            visibleCount++;
        } else {
            $(this).hide().removeClass('fade-in');
        }
    });

    updateResultsCount(visibleCount);
}

// Update results count
function updateResultsCount(count) {
    const hasFilters = $('#movieSearch').val() || $('#categoryFilter').val() || $('#yearFilter').val();

    // Remove existing results text
    $('.results-count').remove();

    if (hasFilters) {
        const resultsText = `<div class="results-count mb-3">
            <div class="alert alert-info">
                <i class="fas fa-filter me-2"></i>
                Showing <strong>${count}</strong> movie(s) matching your filters
            </div>
        </div>`;

        $('.movie-grid-container').before(resultsText);
    }
}

// Debounce function for search
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Animation initialization
function initializeAnimations() {
    // Add hover effects to movie cards
    $('.movie-card').hover(
        function () {
            $(this).addClass('movie-card-hover');
        },
        function () {
            $(this).removeClass('movie-card-hover');
        }
    );

    // Animate elements on scroll
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver(function (entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('fade-in');
            }
        });
    }, observerOptions);

    // Observe movie cards and stat cards
    document.querySelectorAll('.movie-card-wrapper, .stat-card').forEach(el => {
        observer.observe(el);
    });
}

// Notification system
function showNotification(message, type = 'info', duration = 5000) {
    const alertClass = `alert-${type}`;
    const iconClass = getIconForType(type);

    const notification = $(`
        <div class="alert ${alertClass} alert-dismissible fade show position-fixed" 
             style="top: 20px; right: 20px; z-index: 9999; min-width: 300px; max-width: 400px;">
            <i class="fas fa-${iconClass} me-2"></i>
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `);

    $('body').append(notification);

    setTimeout(() => {
        notification.fadeOut(() => notification.remove());
    }, duration);
}

// Get icon for notification type
function getIconForType(type) {
    const icons = {
        'success': 'check-circle',
        'danger': 'exclamation-circle',
        'error': 'exclamation-circle',
        'warning': 'exclamation-triangle',
        'info': 'info-circle'
    };
    return icons[type] || 'info-circle';
}

// Form reset functionality
function resetForm() {
    if (confirm('Are you sure you want to reset the form? All entered data will be lost.')) {
        document.querySelector('.movie-form').reset();
        resetImagePreview();
        $('#selectedActors').empty();
        $('#descriptionCount').text('0');
        $('.is-valid, .is-invalid').removeClass('is-valid is-invalid');
        $('.custom-error').remove();
        showNotification('Form has been reset', 'info');
    }
}

// Export functionality
function exportMovieData() {
    const movies = [];
    $('.movie-card-wrapper:visible').each(function () {
        const card = $(this);
        const movie = {
            name: card.find('.movie-name').text(),
            category: card.data('category'),
            year: card.data('year'),
            producer: card.find('.movie-producer').text().replace('Producer: ', ''),
            description: card.find('.movie-description').text()
        };
        movies.push(movie);
    });

    if (movies.length === 0) {
        showNotification('No movies to export', 'warning');
        return;
    }

    // Convert to CSV
    const csvContent = convertToCSV(movies);
    downloadCSV(csvContent, 'movies.csv');
    showNotification(`Exported ${movies.length} movies to CSV`, 'success');
}

// Convert data to CSV
function convertToCSV(data) {
    const headers = Object.keys(data[0]);
    const csvRows = [headers.join(',')];

    data.forEach(row => {
        const values = headers.map(header => {
            const value = row[header] || '';
            return `"${value.toString().replace(/"/g, '""')}"`;
        });
        csvRows.push(values.join(','));
    });

    return csvRows.join('\n');
}

// Download CSV file
function downloadCSV(csvContent, filename) {
    const blob = new Blob([csvContent], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
}

// Print functionality
function printMovieList() {
    const printWindow = window.open('', '_blank');
    const movieCards = $('.movie-card-wrapper:visible').clone();

    printWindow.document.write(`
        <html>
        <head>
            <title>Movie List</title>
            <style>
                body { font-family: Arial, sans-serif; }
                .movie-card { border: 1px solid #ddd; margin: 10px; padding: 10px; }
                .movie-name { font-weight: bold; font-size: 18px; }
                .movie-info { margin: 5px 0; }
                @media print { .no-print { display: none; } }
            </style>
        </head>
        <body>
            <h1>Movie List</h1>
            <div id="movies"></div>
        </body>
        </html>
    `);

    movieCards.each(function () {
        const name = $(this).find('.movie-name').text();
        const category = $(this).data('category');
        const year = $(this).data('year');
        const description = $(this).find('.movie-description').text();

        printWindow.document.getElementById('movies').innerHTML += `
            <div class="movie-card">
                <div class="movie-name">${name}</div>
                <div class="movie-info">Category: ${category}</div>
                <div class="movie-info">Year: ${year}</div>
                <div class="movie-info">Description: ${description}</div>
            </div>
        `;
    });

    printWindow.document.close();
    printWindow.print();
}

// Keyboard shortcuts
$(document).on('keydown', function (e) {
    // Ctrl/Cmd + N for new movie
    if ((e.ctrlKey || e.metaKey) && e.key === 'n') {
        e.preventDefault();
        if ($('#addNewMovieBtn').length) {
            window.location.href = $('#addNewMovieBtn').attr('href');
        }
    }

    // Ctrl/Cmd + F for search
    if ((e.ctrlKey || e.metaKey) && e.key === 'f') {
        e.preventDefault();
        $('#movieSearch').focus();
    }

    // Escape to clear search
    if (e.key === 'Escape') {
        $('#movieSearch').val('').trigger('input');
    }
});

// Initialize on page load
$(window).on('load', function () {
    // Hide loading overlay if present
    $('.loading-overlay').fadeOut();

    // Initialize character count on edit forms
    if ($('#Description').val()) {
        $('#descriptionCount').text($('#Description').val().length);
    }
});

// Handle page visibility change
document.addEventListener('visibilitychange', function () {
    if (document.visibilityState === 'visible') {
        // Refresh data if page becomes visible after being hidden
        // This could trigger an AJAX call to refresh movie data
    }
});

// Service worker registration for offline functionality (optional)
if ('serviceWorker' in navigator) {
    window.addEventListener('load', function () {
        navigator.serviceWorker.register('/sw.js').then(function (registration) {
            console.log('ServiceWorker registration successful');
        }).catch(function (err) {
            console.log('ServiceWorker registration failed');
        });
    });
}

